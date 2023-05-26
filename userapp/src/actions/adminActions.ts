import axios from 'axios';
import { Dispatch } from 'redux';
import { Admin, actionTypes  } from '../types/types';
import { SET_ADMIN , LIST_CONTRACT ,  MARK_EXECUTE } from '../types/types';
import  jwtAxios  from "../services/auth/jwt-auth";
import  {setAuthToken   }  from "../services/auth/jwt-auth";

export const signInAdmin = ({ email, password }: 
    { email: string; password: string }) => {
    return async (dispatch: Dispatch) => {
      try {
        // Navigate to the Contract page
        const { data } = await jwtAxios.post("/api/admin/auth", { email, password });
  
        console.log("signInAdmin access_token="+ data.access_token);
  
        localStorage.setItem("token", data.access_token);
  
        setAuthToken(data.access_token);
  
        const res = await jwtAxios.get("/api/admin/auth");

        console.log("signInAdmin get method=" + JSON.stringify(res.data));
        
        // Dispatch the set user action
        dispatch(setAdmin(res.data));
  
        console.log("signInAdmin finish to assign");
  
      } catch (error) {
        console.error("signInAdmin=", error);
  
      }
    };
  };

  // Define the user action creators
    export const setAdmin = (admin: Admin ) => ({
        type: SET_ADMIN,
        payload: admin,
    });


  export const markContractExecuted = (  contractId: number
    ) => {
    return async (dispatch: Dispatch<actionTypes>) => {
      try {
  
        console.log("markContractExecuted start=");
        console.log("markContractExecuted start="+contractId);
  
        const response = await jwtAxios.post('/api/contract/execute', {contractId});
        // Assuming the response contains the success message
        const successMessage = response.data.message;
        // Dispatch the success action with the success message
        dispatch({
          type: MARK_EXECUTE,
          payload: successMessage,
        });
      } catch (error) {
        // Dispatch the error action if the request fails
        dispatch({
          type: MARK_EXECUTE,
          payload: 0,
        });
      }
    };
  };

  export const listContracts = () => {
    return async (dispatch: Dispatch<actionTypes>) => {
      try {
  
        console.log("listContracts start=");
        const response = await jwtAxios.get('/api/contract');

        console.log("listContracts start=" + JSON.stringify(response.data));

        // Dispatch the success action with the success message
        dispatch({
          type: LIST_CONTRACT,
          payload:  response.data,
        });
      } catch (error) {
        // Dispatch the error action if the request fails
        dispatch({
          type: LIST_CONTRACT,
          payload: [],
        });
      }
    };
  };
