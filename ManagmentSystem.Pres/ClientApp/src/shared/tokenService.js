import { jwtDecode } from 'jwt-decode';

const tokenService = {
  decodeToken: (token) => {
    try {
      const decoded = jwtDecode(token);
      return decoded;
    } catch (error) {
      return null;
    }
  },
  isTokenExpired: (token) => {
    const decoded = jwtDecode(token);
    const currentTime = Date.now() / 1000; // Current time in seconds
    return decoded.exp < currentTime; // Check if the expiration time is less than current time
  },
};

export default tokenService;
