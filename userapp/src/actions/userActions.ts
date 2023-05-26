import axios from 'axios';
import { Dispatch } from 'redux';
import { User, actionTypes , RequestContract   } from '../types/types';
import { SET_USER  , REGISTER_USER , GET_FX_RATE , MAKE_CONTRACT } from '../types/types';
import  jwtAxios  from "../services/auth/jwt-auth";
import  {setAuthToken   }  from "../services/auth/jwt-auth";


export const registerUser =   (registerRequest: User) => {
  console.log("registerUser start");
  return (dispatch: Dispatch<actionTypes>): Promise<actionTypes> => {
    console.log("registerUser 2 start");


    return jwtAxios
      .post('/api/user/register',  registerRequest  )
      .then((response) => {
        if (response.status === 200) {
          dispatch({
            type: REGISTER_USER,
            payload: response.data,
          });
          return Promise.resolve(response.data);
        } else {
          // Handle the error case if needed
          return Promise.reject(new Error('Something went wrong'));
        }
      })
      .catch((error) => {
        // Dispatch the error action if the request fails
        dispatch({
          type: REGISTER_USER,
          payload: {
            firstName: '',
            lastName: '',
            email: '',
            password: '',
          },
        });
        return Promise.reject(error);
      });
  };
};

export const signInUser = ({ email, password }: 
  { email: string; password: string }) => {
  return async (dispatch: Dispatch) => {
    try {
      // Navigate to the Contract page
      const { data } = await jwtAxios.post("/api/user/auth", { email, password });

      console.log("signInUser access_token=", data.access_token);

      localStorage.setItem("token", data.access_token);

      setAuthToken(data.access_token);

      const res = await jwtAxios.get("/api/user/auth");
      
      console.log("signInUser finish to setUser" + JSON.stringify(res));

      dispatch(setUser(res.data));

      console.log("signInUser finish to setUser");


    } catch (error) {
      console.error("signInUser=", error);

    }
  };
};

// Define the user action creators
export const setUser = (user: User ) => ({
  type: SET_USER,
  payload: user,
});

// Action Creators
export const getFxRate = (fromCCY: string, convertCCY: string) => {
  return async (dispatch: Dispatch<actionTypes>) => {
    try {
      const response = await jwtAxios.get(`https://localhost:7135/api/fxrate?fromCCY=${fromCCY}&convertCCY=${convertCCY}`);
      const fxRate = response.data; // Assuming the response contains the FX rate
      // Dispatch the success action with the FX rate
      dispatch({
        type: GET_FX_RATE,
        payload: fxRate,
      });
    } catch (error) {
      // Dispatch the error action if the request fails
      dispatch({
        type: GET_FX_RATE,
        payload:  {
          denominator: fromCCY,
          numerator: convertCCY,
          rate: "NA",
          lastUpdateDate: new Date()
        },
      });
    }
  };
};

export const makeContract = (  requestContract: RequestContract
  ) => {
  return async (dispatch: Dispatch<actionTypes>) => {
    try {

      console.log("makeContract start");
      console.log("makeContract start="+JSON.stringify(requestContract));

      const response = await jwtAxios.post('/api/contract', requestContract);
      // Assuming the response contains the success message
      const successMessage = response.data;

      console.log("makeContract successMessage="+JSON.stringify(response.data));

      // Dispatch the success action with the success message
      dispatch({
        type: MAKE_CONTRACT,
        payload: successMessage,
      });
    } catch (error) {
      console.log("error="+error)
      console.log(error)
      // Dispatch the error action if the request fails
      dispatch({
        type: MAKE_CONTRACT,
        payload: 0,
      });
    }
  };
};