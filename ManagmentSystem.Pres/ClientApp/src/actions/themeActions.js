/* eslint-disable prettier/prettier */

export const TOGGLE_THEME = 'TOGGLE_THEME'

export const changeTheme = (theme, sidebarShow) => ({
  type: TOGGLE_THEME,
  payload: { theme, sidebarShow }
});

export const toggleTheme = (themeObj) => {
  const { theme, sidebarShow } = themeObj
  return (dispatch) => {
    dispatch(changeTheme(theme, sidebarShow))
  }
}
