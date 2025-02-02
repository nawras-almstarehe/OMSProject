import axios from 'axios';
import axiosRetry from 'axios-retry'; // Import axios-retry

// Create an Axios instance
const api = axios.create({
  baseURL: process.env.REACT_APP_API_URL, // Replace with your API base URL
  timeout: 10000, // Timeout in milliseconds
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add a request interceptor
api.interceptors.request.use(
  (config) => {
    // Add authorization token or other headers if needed
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Add a response interceptor
api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error.response?.status === 401) {
      // Redirect to login or clear tokens
      localStorage.removeItem('token');
      window.location.href = '/login';
    }
    // Handle errors globally
    if (error.response) {
      // Server responded with a status outside the 2xx range
      console.error('API Error:', error.response.data);
    } else {
      // No response received
      console.error('Network Error:', error.message);
    }
    return Promise.reject(error);
  }
);

// Add retry logic using axios-retry
//axiosRetry(api, {
//  retries: 3, // Retry up to 3 times
//  retryCondition: (error) => {
//    // Retry only if the request failed due to a network error or 5xx server error
//    return error.response?.status >= 500 || !error.response;
//  },
//  retryDelay: (retryCount) => {
//    // Exponential backoff: 1000ms, 2000ms, 4000ms...
//    return retryCount * 1000;
//  },
//});

export default api;




//NOTS  ---->   Implement retry logic for failed requests (e.g., network issues):
/*
npm install axios-retry

import axiosRetry from 'axios-retry';
axiosRetry(api, { retries: 3 });
*/
