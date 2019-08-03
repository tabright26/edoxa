import React from 'react';
import { Redirect } from 'react-router-dom';

const ForbiddenRedirect = () => <Redirect path="/forbidden" />;

export default ForbiddenRedirect;
