import { actionTypes, User ,  SET_USER , MAKE_CONTRACT  } from '../types/types';

const initialState:  {
  isLoggedIn: boolean,
  userDetails:  User | null ,  
  message: string ,
} = {
  isLoggedIn: false,
  userDetails: null,
  message: "" ,
};

const userReducer = (state = initialState, 
  action: actionTypes) => {
  switch (action.type) {         
    case SET_USER:

      console.log("userReducer action.payload=" + JSON.stringify(action.payload))

      return {
        ...state,
        userDetails: action.payload,
        isLoggedIn: action.payload !== null,
      };      
     

    default:
      return state;
  }
};

export default userReducer;
