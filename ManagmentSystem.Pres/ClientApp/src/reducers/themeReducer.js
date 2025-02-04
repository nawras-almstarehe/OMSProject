import { TOGGLE_THEME } from '../actions/themeActions'

const initialState = {
  sidebarShow: true,
  theme: 'light',
  sidebarUnfoldable: true
}

const themeReducer = (state = initialState, action) => {
  switch (action.type) {
    case TOGGLE_THEME:
      return {
        ...state,
        sidebarShow: action.payload.sidebarShow,
        sidebarUnfoldable: action.payload.sidebarUnfoldable,
        theme: action.payload.theme,
      }
    default:
      return state
  }
}

export default themeReducer
