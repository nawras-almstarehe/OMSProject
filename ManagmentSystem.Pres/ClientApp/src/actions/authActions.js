// src/actions/authActions.js
export const LOGIN_SUCCESS = 'LOGIN_SUCCESS'
export const LOGOUT = 'LOGOUT'
export const LOGIN_FAILURE = 'LOGIN_FAILURE'

export const loginSuccess = (Username, token, Email, PriviligeCode) => ({
  type: LOGIN_SUCCESS,
  payload: { Username, token, Email, PriviligeCode },
})

export const logout = () => ({
  type: LOGOUT,
})

// Create an action for login failure
export const loginFailure = (error) => ({
  type: LOGIN_FAILURE,
  payload: error,
})

// Async action for logging in
export const login = (credentials) => {
  debugger
  return async (dispatch) => {
    const { userName, password } = credentials
    const UserObj = {
      Username: userName,
      Password: password,
      Email: ''
    }
    // Mock API call (replace with your actual API call)
    // Here you would make a POST request to your authentication endpoint.

    const response = await fetch('https://localhost:44329/api/Users/Login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(UserObj),
    });

    if (!response.ok) {
      const errorData = await response.json();
      dispatch(loginFailure(errorData))
    }

    //const response1 = await new Promise((resolve, reject) => {
    //  debugger
    //  setTimeout(() => {
    //    if (userName === 'user@example.com' && password === 'password') {
    //      resolve({ user: { userName }, token: 'mocked-token-12345' })
    //    } else {
    //      reject('Invalid credentials')
    //    }
    //  }, 1000)
    //})

    // Handle the response
    try {
      debugger
      const responseData = await response.json();
      const { username, token, email, priviligecode } = responseData
      dispatch(loginSuccess(username, token, email, priviligecode))
      localStorage.setItem('token', token) // Store token in localStorage
      localStorage.setItem('Username', username) // Store user info
    } catch (error) {
      dispatch(loginFailure(error))
    }
  }
}

// Action for logging out
export const userLogout = () => {
  return (dispatch) => {
    dispatch(logout())
    localStorage.removeItem('token') // Clear token from storage
    localStorage.removeItem('Username') // Clear user info
  }
}
