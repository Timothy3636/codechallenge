import React, { useState } from 'react';
import axios from 'axios';
import {signInUser} from '../../actions/userActions'
import { useDispatch } from 'react-redux';
import { Navigate } from 'react-router-dom';
import Contract from './Contract';
import { RootState } from '../../reducers/rootReducer';
import { useSelector } from 'react-redux';

const LoginPage: React.FC = () => {
  
  const dispatch = useDispatch();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  
  const isLoggedIn = useSelector((state: RootState) => state.user.isLoggedIn);


  const handleLogin = async () => {
      await dispatch(signInUser({"email":email, 
      "password": password}) as any);
  };

  // If loggedIn is true, redirect to the desired page
  if (isLoggedIn) {
    return <Navigate to="/Contract" />;
  }  


  return (
    <div className="container">
      <h1>Login</h1>
      <table className="login-table">
        <tbody>
          <tr>
            <td>
              <label htmlFor="email">Email:</label>
            </td>
            <td>
              <input
                type="email"
                id="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="Enter your email"
                required
              />
            </td>
          </tr>
          <tr>
            <td>
              <label htmlFor="password">Password:</label>
            </td>
            <td>
              <input
                type="password"
                id="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Enter your password"
                required
              />
            </td>
          </tr>
        </tbody>
      </table>
      <button onClick={handleLogin}>Login</button>
    </div>
  );
};

export default LoginPage;
