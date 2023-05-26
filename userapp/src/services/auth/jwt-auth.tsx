import axios from "axios";
import { User   } from '../../types/types';

export const jwtAxios = axios.create({
    baseURL:  "https://localhost:7135",
    headers: {
      "Content-Type": "application/json",
    },
  });
  
  
  jwtAxios.interceptors.response.use(
    (res) => {      
      // console.log('jwtAxios Request:'+ JSON.stringify(res));
      return res
    }  ,
    (err) => {
      if (err.response && err.response.data.msg === "Token is not valid") {
        console.log("Need to logout user");
        // store.dispatch({type: LOGOUT});
      }
      return Promise.reject(err);
    }
  );
  

  export const setAuthToken = (token ?: string) => {    
    if (token) {    
        jwtAxios.defaults.headers.common['Authorization'] = 'Bearer ' + token;// Change this according your requirement

      localStorage.setItem('token', token);
    } else {    
      delete jwtAxios.defaults.headers.common['Authorization'];
      
      localStorage.removeItem('token');
    }
  };

  
  export const setAdminAuthToken = (token ?: string) => {    
    if (token) {    
        jwtAxios.defaults.headers.common['Authorization'] = 'Bearer ' + token;// Change this according your requirement

      localStorage.setItem('adminToken', token);
    } else {    
      delete jwtAxios.defaults.headers.common['Authorization'];
      
      localStorage.removeItem('adminToken');
    }
  };


  // export const seUserProfile = (user ?: User) => {
  //   if (user) {   
  //     localStorage.setItem('user', user);
  //   } else {    
  //     localStorage.removeItem('user');
  //   }
  // };

  // export const setUserProfile = (user?: User) => {
  //   console.log("setUserProfile start=")
  //   console.log("setUserProfile user="+user?.userID)
  //   if (user) {
  //     const userProfile = JSON.stringify(user);
  //     localStorage.setItem('user', userProfile);
  //   } else {
  //     localStorage.removeItem('user');
  //   }
  // };
  
  // export const getUserProfile = () => {
  //   const userProfile = localStorage.getItem('user');
  //   if (userProfile) {
  //     return JSON.parse(userProfile);
  //   } else {
  //     return null;
  //   }
  // };
  
  
  export const getuser = (token ?: string) => {
    if (token) {    
        jwtAxios.defaults.headers.common['Authorization'] = 'Bearer ' + token;// Change this according your requirement

      localStorage.setItem('token', token);
    } else {    
      delete jwtAxios.defaults.headers.common['Authorization'];
      
      localStorage.removeItem('token');
    }
  };
  
  
  export default jwtAxios;

  


