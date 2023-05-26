import { RequestContract , actionTypes, Admin ,  SET_ADMIN , LIST_CONTRACT  } from '../types/types';

const initialState:  {
  isLoggedIn: boolean,
  contractLists :  RequestContract[] | null ,  
} = {
  isLoggedIn: false,
  contractLists :  [],  
};

const adminReducer = (state = initialState, 
  action: actionTypes) => {
  switch (action.type) {         
    case LIST_CONTRACT:

      return {
        ...state,
        contractLists: action.payload,
        isLoggedIn: action.payload !== null,
      };      
    default:
      return state;
  }
};

export default adminReducer;
