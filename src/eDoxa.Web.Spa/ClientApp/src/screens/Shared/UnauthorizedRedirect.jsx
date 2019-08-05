import React from 'react';
import { Redirect } from 'react-router-dom';

const UnauthorizedRedirect = () => <Redirect path="/unauthorized" />;

export default UnauthorizedRedirect;
