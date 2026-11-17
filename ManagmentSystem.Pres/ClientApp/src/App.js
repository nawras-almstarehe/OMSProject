import React, { Suspense, useEffect } from 'react';
import { Navigate, HashRouter, Route, Routes } from 'react-router-dom';
import { useSelector } from 'react-redux';
import i18n from 'i18next';
import apiService from '../src/shared/apiService';
import { CSpinner, useColorModes } from '@coreui/react';
import './scss/style.scss';

// Containers
const DefaultLayout = React.lazy(() => import('./layout/DefaultLayout'));

// Pages
const Login = React.lazy(() => import('./views/pages/login/Login'));
const Register = React.lazy(() => import('./views/pages/register/Register'));
const Page404 = React.lazy(() => import('./views/pages/page404/Page404'));
const Page500 = React.lazy(() => import('./views/pages/page500/Page500'));

//Shared
const TokenCheck = React.lazy(() => import('./shared/TokenCheck'));

async function initializeLanguage() {
  try {
    const response = await apiService.get('api/Config/language');
    const { language } = response;
    i18n.changeLanguage(language);
  } catch (error) {
    console.error('Failed to fetch language:', error);
  }
}


const App = () => {
  const { isColorModeSet, setColorMode } = useColorModes('coreui-free-react-admin-template-theme');
  const storedTheme = useSelector((state) => state.theme.theme);
  const token = useSelector((state) => state.auth.token);

  useEffect(() => {
    initializeLanguage();
    const urlParams = new URLSearchParams(window.location.href.split('?')[1]);
    const theme = urlParams.get('theme') && urlParams.get('theme').match(/^[A-Za-z0-9\s]+/)[0];
    if (theme) {
      setColorMode(theme);
    }
    if (isColorModeSet()) {
      return;
    }
    setColorMode(storedTheme)
  }, [])

  return (
    <HashRouter>
      <TokenCheck />
      <Suspense
        fallback={
          <div className="pt-3 text-center">
            <CSpinner color="primary" variant="grow" />
          </div>
        }
      >
        <Routes>
          <Route
            exact
            path="/login"
            name="Login Page"
            element={token ? <Navigate to="/" /> : <Login />}
          />
          <Route exact path="/register" name="Register Page" element={<Register />} />
          <Route exact path="/404" name="Page 404" element={<Page404 />} />
          <Route exact path="/500" name="Page 500" element={<Page500 />} />
          <Route
            path="*"
            name="Home"
            element={token ? <DefaultLayout /> : <Navigate to="/login" />}
          />
        </Routes>
      </Suspense>
    </HashRouter>
  )
}

export default App;
