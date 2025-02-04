/* eslint-disable prettier/prettier */

export const TOGGLE_THEME = 'TOGGLE_THEME'

export const changeTheme = (theme, sidebarShow, sidebarUnfoldable) => ({
  type: TOGGLE_THEME,
  payload: { theme, sidebarShow, sidebarUnfoldable }
});

export const toggleTheme = (themeObj) => {
  const { theme, sidebarShow, sidebarUnfoldable } = themeObj
  return (dispatch) => {
    dispatch(changeTheme(theme, sidebarShow, sidebarUnfoldable))
  }
}
