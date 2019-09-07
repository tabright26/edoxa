import React from "react";

//Change these into pages

const FAQ = React.lazy(() => import("../../../../components/Page/FAQ"));
const TermsOfServices = React.lazy(() => import("../../../../components/Page/TermsOfServices"));

// https://github.com/ReactTraining/react-router/tree/master/packages/react-router-config
const routes = [
  { path: "/information/faq", name: "F. A. Q.", component: FAQ, exact: true, secure: false, scopes: [] },
  { path: "/information/terms-of-services", name: "Terms of Services", component: TermsOfServices, exact: true, secure: false, scopes: [] }
];

export default routes;
