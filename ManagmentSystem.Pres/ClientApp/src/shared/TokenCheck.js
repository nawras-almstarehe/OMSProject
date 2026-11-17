// src/components/TokenCheck.js
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { loginSuccess, userLogout } from '../actions/authActions';
import tokenService from '../shared/tokenService';
import { useNavigate } from 'react-router-dom';

const TokenCheck = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    const user = localStorage.getItem('Username');
    const token = localStorage.getItem('token');

    if (user && token) {
      const isTokenExp = tokenService.isTokenExpired(token);
      if (!isTokenExp) {
        dispatch(loginSuccess(user, token)); // Restore user session
      } else {
        dispatch(userLogout());
        navigate('/login'); // Navigate back to login after logout
      }
    }
  }, [dispatch, navigate]);

  return null; // This component does not render anything
};

export default TokenCheck;
