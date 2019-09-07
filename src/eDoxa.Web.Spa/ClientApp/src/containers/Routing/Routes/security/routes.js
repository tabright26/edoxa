import React from "react";

//Change these into views

const SecurityEmailConfirm = React.lazy(() => import("../../../../components/Page/Security/Email/Comfirm"));
const SecurityPasswordForgot = React.lazy(() => import("../../../../components/Page/Security/Password/Forgot"));
const SecurityPasswordReset = React.lazy(() => import("../../../../components/Page/Security/Password/Reset"));

// https://github.com/ReactTraining/react-router/tree/master/packages/react-router-config
const routes = [
  { path: "/security/email/confirm", name: "Confirm Email", component: SecurityEmailConfirm, exact: true, secure: false, scopes: [] },
  { path: "/security/password/forgot", name: "Forgot Password", component: SecurityPasswordForgot, exact: true, secure: false, scopes: [] },
  { path: "/security/password/reset", name: "Reset Password", component: SecurityPasswordReset, exact: true, secure: false, scopes: [] }
];

export default routes;
