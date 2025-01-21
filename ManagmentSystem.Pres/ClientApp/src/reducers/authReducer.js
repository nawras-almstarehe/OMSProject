// src/reducers/authReducer.js
import { LOGIN_SUCCESS, LOGOUT, LOGIN_FAILURE } from '../actions/authActions'

const initialState = {
  user: null,
  token: null,
  Email: null,
  PriviligeCode: null,
  error: null,
}

const authReducer = (state = initialState, action) => {
  switch (action.type) {
    case LOGIN_SUCCESS:
      return {
        ...state,
        user: action.payload.Username,
        token: action.payload.token,
        Email: action.payload.Email,
        PriviligeCode: action.payload.PriviligeCode,
        error: null,
      }
    case LOGOUT:
      return {
        ...state,
        user: null,
        token: null,
        Email: null,
        PriviligeCode: null,
      }
    case LOGIN_FAILURE:
      return {
        ...state,
        error: action.payload,
      }
    default:
      return state
  }
}

export default authReducer
