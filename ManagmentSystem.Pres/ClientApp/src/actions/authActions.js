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

export const loginFailure = (error) => ({
  type: LOGIN_FAILURE,
  payload: error,
})

export const login = (credentials) => {
  return async (dispatch) => {
    const { userName, password } = credentials
    const UserObj = {
      Username: userName,
      Password: password,
      Email: ''
    }

    const response = await fetch('https://localhost:44329/api/Users/Login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(UserObj),
    });

    if (!response.ok) {
      const errorData = await response.json();
      dispatch(loginFailure(errorData.message))
    }
    try {
      const responseData = await response.json();
      const { username, token, email, priviligeCode } = responseData.data
      dispatch(loginSuccess(username, token, email, priviligeCode))
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
