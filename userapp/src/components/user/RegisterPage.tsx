import React, { useState } from 'react';
import './RegisterPage.css';
import { useDispatch } from 'react-redux';
import { registerUser } from '../../actions/userActions';
import { User } from '../../types/types';

const RegisterPage: React.FC = () => {
  const dispatch = useDispatch();
  const [formData, setFormData] = useState({
    firstName: 'Tim',
    lastName: 'Yu',
    email: 'yu_timothy@hotmail.com',
    password: '123',
    reconfirmedPassword: '123',
  });
  const [errors, setErrors] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    reconfirmedPassword: '',
  });
  const [successMessage, setSuccessMessage] = useState('');

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  }; 

  const validateForm = () => {
    let isValid = true;
    const updatedErrors = {
      firstName: '',
      lastName: '',
      email: '',
      password: '',
      reconfirmedPassword: '',
    };

    if (!formData.firstName) {
      updatedErrors.firstName = 'First Name is required';
      isValid = false;
    }

    if (!formData.lastName) {
      updatedErrors.lastName = 'Last Name is required';
      isValid = false;
    }

    if (!formData.email) {
      updatedErrors.email = 'Email is required';
      isValid = false;
    } else if (!isValidEmail(formData.email)) {
      updatedErrors.email = 'Please enter a valid email address';
      isValid = false;
    }

    if (!formData.password) {
      updatedErrors.password = 'Password is required';
      isValid = false;
    }

    if (formData.password !== formData.reconfirmedPassword) {
      updatedErrors.reconfirmedPassword = 'Passwords do not match';
      isValid = false;
    }

    setErrors(updatedErrors);
    return isValid;
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {

  
    e.preventDefault();
    if (validateForm()) {
      // Call the API to register the user
      const registerRequest: User = {
        firstName: formData.firstName,
        lastName: formData.lastName,
        email: formData.email,
        password: formData.password,
      };

      dispatch(registerUser(registerRequest) as any);      
      // Set success message and clear form data
      setSuccessMessage('Registration successful');
      setFormData({
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        reconfirmedPassword: '',
      });
      setErrors({
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        reconfirmedPassword: '',
      });
    }
  };

  const isValidEmail = (email: string): boolean => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  };
  

  return (
    <div className="register-container">
      <h1 className="register-heading">Register</h1>
      <form onSubmit={handleSubmit}>
        <table className="register-form-table">
          <tbody>
            <tr>
              <td>
                <label htmlFor="firstName">First Name:</label>
              </td>
              <td>
                <input
                  type="text"
                  id="firstName"
                  name="firstName"
                  value={formData.firstName}
                  onChange={handleInputChange}
                  placeholder="Enter your first name"
                  required
                />
                {errors.firstName && <span className="error-message">{errors.firstName}</span>}
              </td>
            </tr>
            <tr>
              <td>
                <label htmlFor="lastName">Last Name:</label>
              </td>
              <td>
                <input
                  type="text"
                  id="lastName"
                  name="lastName"
                  value={formData.lastName}
                  onChange={handleInputChange}
                  placeholder="Enter your last name"
                  required
                />
                {errors.lastName && <span className="error-message">{errors.lastName}</span>}
              </td>
            </tr>
            <tr>
              <td>
                <label htmlFor="email">Email:</label>
              </td>
              <td>
                <input
                  type="email"
                  id="email"
                  name="email"
                  value={formData.email}
                  onChange={handleInputChange}
                  placeholder="Enter your email"
                  required
                />
                {errors.email && <span className="error-message">{errors.email}</span>}
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
                  name="password"
                  value={formData.password}
                  onChange={handleInputChange}
                  placeholder="Enter your password"
                  required
                />
                {errors.password && <span className="error-message">{errors.password}</span>}
              </td>
            </tr>
            <tr>
              <td>
                <label htmlFor="reconfirmedPassword">Reconfirmed Password:</label>
              </td>
              <td>
                <input
                  type="password"
                  id="reconfirmedPassword"
                  name="reconfirmedPassword"
                  value={formData.reconfirmedPassword}
                  onChange={handleInputChange}
                  placeholder="Re-enter your password"
                  required
                />
                {errors.reconfirmedPassword && (
                  <span className="error-message">{errors.reconfirmedPassword}</span>
                )}
              </td>
            </tr>
          </tbody>
        </table>
        <button type="submit" className="register-button">
          Register
        </button>
      </form>
      {successMessage && <div className="success-message">{successMessage}</div>}
    </div>
  );
};

export default RegisterPage;
