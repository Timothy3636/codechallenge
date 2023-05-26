import React, { useState, useEffect } from 'react';
import {  listContracts ,  markContractExecuted } from '../../actions/adminActions';
import { useSelector, useDispatch } from 'react-redux';
import { RootState } from '../../reducers/rootReducer';

export interface RequestContract {
  UserID?: number;
  ConvertToCurrency: string;
  FundFromCurrency: string;
  FundFromAmount: number;
  ConvertToAmount: number;
  ExchangeRate: number;
  ContacttId?: number;
}

const ContractList: React.FC = () => {
  const dispatch = useDispatch();
  const contracts = useSelector((state: RootState) => state.admin.contractLists); // Assuming contracts is stored in the contract reducer

  useEffect(() => {
    // Dispatch the listContracts action to fetch the contract list
    dispatch(listContracts() as any);
  }, [dispatch]);


  const callMarkContractExecuted = async (contractId?: number): Promise<void> => {
    if (contractId) {
      // Prompt a confirmation message
      const confirmed = window.confirm('Are you sure you want to mark this contract as executed?');
      if (confirmed) {
        // Implement the API call to mark the contract as executed
        // You can use fetch or any HTTP library to make the API request

        dispatch(markContractExecuted(contractId) as any);

        dispatch(listContracts() as any);
      }
    }
  };

  return (
    <div>
      <table>
        <thead>
          <tr>
            <th>RequestContract</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
        {contracts && contracts.length > 0 ? (
            contracts.map((contract) => (
                <tr key={contract.ContacttId}>
                <td>{contract.UserID ?? '-'}</td>
                <td>{contract.ConvertToCurrency ?? '-'}</td>
                <td>{contract.FundFromCurrency ?? '-'}</td>
                <td>{contract.FundFromAmount ?? '-'}</td>
                <td>{contract.ConvertToAmount ?? '-'}</td>
                <td>{contract.ExchangeRate ?? '-'}</td>
                <td>
                    <button onClick={() => callMarkContractExecuted(contract.ContacttId)}>Approve</button>
                </td>
                </tr>
            ))
            ) : (
            <tr>
                <td colSpan={7}>No contracts available.</td>
            </tr>
            )}

        </tbody>
      </table>
    </div>
  );
};

export default ContractList;
