import React, { useEffect , useState  } from 'react';
import { useDispatch } from 'react-redux';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import RegisterPage from './components/user/RegisterPage';
import LoginPage from './components/user/LoginPage';
import Contract from './components/user/Contract';

import AdminLoginPage from './components/admin/LoginPage';
import ContractList from './components/admin/ContractList';
import { useSelector } from 'react-redux';
import { RootState } from './reducers/rootReducer';

function App() {

  const [isAdmin, setIsAdmin] = useState(true);


  const checkAdminStatus = (pathname: string): boolean => {
    return pathname.includes('Admin');
  };  
  const isLoggedIn = useSelector((state: RootState) => state.user.isLoggedIn);
  // const isAdminLoggedIn = useSelector((state: RootState) => state.admin.isLoggedIn);

  return (
    <div className="App">
      <Router>
        <nav>
          { (!isLoggedIn ) && (
            <ul>
              <li>
                <Link to="/register">Register</Link>
              </li>
              <li>
                <Link to="/login">Login</Link>
              </li>    
            </ul>
          )}    
            {isLoggedIn && (
            <ul>
                <li>
                  <Link to="/contract">Contract</Link>
                </li>    
            </ul>
            )}   
            {/* {isAdmin && isAdminLoggedIn && (
            <ul>
                <li>
                  <Link to="/admin/contractlist">Contract</Link>
                </li>    
            </ul>
            )}                */}
        </nav>

        <Routes>
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/login" element={<LoginPage />} />          
          <Route path="/contract" element={<Contract />} />

          <Route path="/admin/login" element={<AdminLoginPage />} />          
          <Route path="/admin/contractlist" element={<ContractList />} />          
          
        </Routes>
      </Router>
    </div>
  );
}

export default App;
