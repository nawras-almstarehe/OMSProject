import { TOGGLE_THEME } from '../actions/themeActions'

const initialState = {
  sidebarShow: true,
  theme: 'light',
}

const themeReducer = (state = initialState, action) => {
  switch (action.type) {
    case TOGGLE_THEME:
      return {
        ...state,
        sidebarShow: action.payload.sidebarShow,
        theme: action.payload.theme,
      }
    default:
      return state
  }
}

export default themeReducer
