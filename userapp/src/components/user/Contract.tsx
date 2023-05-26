import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import {  makeContract } from '../../actions/userActions';
import {   RequestContract   } from "../../types/types";
import { RootState } from '../../reducers/rootReducer';

// import  { getUserProfile  }  from "../../services/auth/jwt-auth";
const ContractPage: React.FC = () => {

  const dispatch = useDispatch();
  const [fromCCY, setFromCCY] = useState('AUD');
  const [convertCCY, setConvertCCY] = useState('HKD');
  const [fromCCYAmt, setFromCCYAmt] = useState(1000);
  const [toCCYAmtStr, setToCCYAmt] = useState('');
  const [fxRate, setFxRate] = useState(0);
  const [contractCreated, setContractCreated] = useState(false);

  const user = useSelector((state: RootState) => state.user.userDetails);
  
  const [message, setMessage] = useState("");

  
  const userId = user?.userId;

  console.log("ContractPage userId="+userId);

  const requestContract: RequestContract = {
    UserID: userId ?? 0 , // Provide a default value if user is null
    ConvertToCurrency: convertCCY,
    FundFromCurrency: fromCCY ,
    FundFromAmount: fromCCYAmt,
    ConvertToAmount: parseFloat(toCCYAmtStr),
    ExchangeRate: fxRate,   
};


  const handleMakeContract = () => {

      dispatch(makeContract(requestContract) as any)
      .then(() => {
        // Set the boolean variable to true when ContractId is returned
        setFromCCYAmt(0);
        setFxRate(0);
        setMessage("Contract submission processed successfully");
      })
      .catch((error: any) => {
        // Handle any error that occurred during the makeContract action
        console.error(error);
        setMessage("Contract submission failed");
      });     
  };

  const handleFromCCYAmtChange = async (amount: number) => {
    setFromCCYAmt(amount);
    const response = await axios.get(`https://localhost:7135/api/fxrate?fromCCY=${fromCCY}&convertCCY=${convertCCY}`);
    const fxRate = response.data.fxRate; // Assuming the response contains the latest FX rate
    setFxRate(fxRate);
    const toCCYAmt = (amount / fxRate).toFixed(4);
    setToCCYAmt(toCCYAmt);
  };
  

  return (
    <div className="container">
      <h1>Make Contract</h1>
      <table className="contract-table">
      <tbody>
        <tr>
          <td>
            <label >From:</label>
          </td>
          <td>
            <select id="fromCCY" value={fromCCY} >
                <option value="AUD">AUD (Australian Dollar)</option>
                <option value="JPY">JPY (Japanese Yen)</option>
                <option value="USD">USD (United States Dollar)</option>
                <option value="HKD">HKD (Hong Kong Dollar)</option>
                <option value="EUR">EUR (Euro)</option>
                <option value="GBP">GBP (British Pound Sterling)</option>
                
            </select>

          </td>
        </tr>
        <tr>          
        <td>
            <label >To:</label>
        </td>            
        <td>
            <select id="convertCCY" value={convertCCY} >
            <option value="HKD">HKD (Hong Kong Dollar)</option>
            <option value="JPY">JPY (Japanese Yen)</option>
            <option value="USD">USD (United States Dollar)</option>
            
            <option value="EUR">EUR (Euro)</option>
            <option value="GBP">GBP (British Pound Sterling)</option>
            <option value="AUD">AUD (Australian Dollar)</option>
            </select>
          </td>         
          </tr>
        <tr>          
            <td>
                <label >Amount:</label>
            </td>               
            <td>
                <input
                    type="text"
                    id="fromCCYAmt"
                    value={fromCCYAmt}
                    onChange={(e) => handleFromCCYAmtChange(parseFloat(e.target.value))}
                    placeholder="Enter amount"
                />
            </td>

        </tr>
        <tr>          
            <td>
                <label>FX Rate:</label>
            </td>               
            <td>
            <label >{fxRate}</label>
            </td>    
        </tr>        
        <tr>
          <td>
            <label >Contract Amount:</label>
          </td>
          <td>
            <label >{convertCCY} {toCCYAmtStr}</label>
          </td>
        </tr>

        <tr>
          <td>
          </td>
          <td>
            <button onClick={handleMakeContract}>Make Contract</button>
          </td>
        </tr>    
        </tbody>    
      </table>

      { <p>{message}</p>}
    </div>
  );
};

export default ContractPage;
