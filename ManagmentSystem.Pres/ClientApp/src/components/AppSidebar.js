import React, { useEffect, useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { toggleTheme } from '../actions/themeActions'
import {
  CCloseButton,
  CSidebar,
  CSidebarBrand,
  CSidebarFooter,
  CSidebarHeader,
  CSidebarToggler,
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { AppSidebarNav } from './AppSidebarNav'
import { logo } from 'src/assets/brand/logo' // School logo
// sidebar nav config
import navigation from '../_nav'
import { useTranslation } from 'react-i18next';

const AppSidebar = (props) => {
  const dispatch = useDispatch()
  const unfoldable = useSelector((state) => state.theme.sidebarUnfoldable)
  const sidebarShow = useSelector((state) => state.theme.sidebarShow)
  const theme = useSelector((state) => state.theme.theme)
  const navItems = navigation();
  const { i18n } = useTranslation();

  return (
    <CSidebar
      className={`border-end ${props.isRTL ? 'sidebar-end' : ''}`}
      colorScheme="dark"
      position="fixed"
      unfoldable={unfoldable}
      visible={sidebarShow}
      onVisibleChange={(visible) => {
        dispatch(toggleTheme({ theme: theme, sidebarShow: visible }))
      }}
    >
      <CSidebarHeader className="border-bottom">
        <CSidebarBrand to="/">
          <CIcon customClassName="sidebar-brand-full" icon={logo} height={32} />
        </CSidebarBrand>
        {/* <span>SCHOOL</span> */}
        <CCloseButton
          className="d-lg-none"
          dark
          onClick={() => dispatch(toggleTheme({ theme: theme, sidebarShow: false }))}
        />
      </CSidebarHeader>
      <AppSidebarNav items={navItems} />
      <CSidebarFooter className="border-top d-none d-lg-flex">
        <CSidebarToggler
          onClick={() => dispatch(toggleTheme({ theme: theme, sidebarShow: sidebarShow, sidebarUnfoldable: !unfoldable }))}
        />
      </CSidebarFooter>
    </CSidebar>
  )
}

export default React.memo(AppSidebar)
