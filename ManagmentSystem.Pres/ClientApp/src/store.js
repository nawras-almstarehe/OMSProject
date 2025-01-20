/* eslint-disable prettier/prettier */
import { legacy_createStore as createStore, applyMiddleware } from 'redux'
import { thunk } from 'redux-thunk'
import rootReducer from './reducers'
import { loginSuccess } from './actions/authActions'
//import { composeWithDevTools } from 'redux-devtools-extension'

const store = createStore(
  rootReducer,
  applyMiddleware(thunk)
)

// Check for saved user and token in localStorage
const user = JSON.parse(localStorage.getItem('user'))
const token = localStorage.getItem('token')

if (user && token) {
  store.dispatch(loginSuccess(user, token)) // Restore user session
}
export default store
