import React, { FunctionComponent, useState, useEffect } from "react";
import MoneyBreadcrumb from "./Money";
import TokenBreadcrumb from "./Token";
import authorizeService from "utils/oidc/AuthorizeService";
import { Breadcrumb } from "reactstrap";

const AppBreadcrumb: FunctionComponent = () => {
  const [authenticated, setAuthenticated] = useState(false);
  useEffect(() => {
    authorizeService.isAuthenticated().then(setAuthenticated);
  }, []);
  return (
    authenticated && (
      <Breadcrumb listClassName="m-0">
        <li className="breadcrumb-menu">
          <div className="btn-group" role="group">
            <MoneyBreadcrumb />
            <TokenBreadcrumb className="ml-3" />
          </div>
        </li>
      </Breadcrumb>
    )
  );
};

export default AppBreadcrumb;
