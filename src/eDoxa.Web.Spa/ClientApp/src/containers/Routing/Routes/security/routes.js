import React from "react";

const SecurityEmailConfirm = React.lazy(() => import("../../../../views/User/Email/Comfirm"));
const SecurityPasswordForgot = React.lazy(() => import("../../../../views/User/Password/Forgot"));
const SecurityPasswordReset = React.lazy(() => import("../../../../views/User/Password/Reset"));

// https://github.com/ReactTraining/react-router/tree/master/packages/react-router-config
const routes = [
  { path: "/security/email/confirm", name: "Confirm Email", component: SecurityEmailConfirm, exact: true, secure: false, scopes: [] },
  { path: "/security/password/forgot", name: "Forgot Password", component: SecurityPasswordForgot, exact: true, secure: false, scopes: [] },
  { path: "/security/password/reset", name: "Reset Password", component: SecurityPasswordReset, exact: true, secure: false, scopes: [] }
];

export default routes;
