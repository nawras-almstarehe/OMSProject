// src/actions/authActions.js
export const LOGIN_SUCCESS = 'LOGIN_SUCCESS'
export const LOGOUT = 'LOGOUT'
export const LOGIN_FAILURE = 'LOGIN_FAILURE'

export const loginSuccess = (user, token) => ({
  type: LOGIN_SUCCESS,
  payload: { user, token },
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
  return async (dispatch) => {
    const { email, password } = credentials

    // Mock API call (replace with your actual API call)
    // Here you would make a POST request to your authentication endpoint.
    const response = await new Promise((resolve, reject) => {
      setTimeout(() => {
        if (email === 'user@example.com' && password === 'password') {
          resolve({ user: { email }, token: 'mocked-token-12345' })
        } else {
          reject('Invalid credentials')
        }
      }, 1000)
    })

    // Handle the response
    try {
      const { user, token } = response
      dispatch(loginSuccess(user, token))
      localStorage.setItem('token', token) // Store token in localStorage
      localStorage.setItem('user', JSON.stringify(user)) // Store user info
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
    localStorage.removeItem('user') // Clear user info
  }
}
