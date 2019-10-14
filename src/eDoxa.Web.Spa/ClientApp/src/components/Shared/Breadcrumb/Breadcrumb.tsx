import React from "react";
import MoneyBreadcrumb from "./Money";
import TokenBreadcrumb from "./Token";
import { withtUser } from "store/root/user/container";

const Breadcrumb: any = ({ isAuthenticated }) => {
  if (isAuthenticated) {
    return (
      <nav className="breadcrumb d-flex">
        <div className="ml-auto">
          <MoneyBreadcrumb />
        </div>
        <div className="ml-3">
          <TokenBreadcrumb />
        </div>
      </nav>
    );
  }
  return null;
};

export default withtUser(Breadcrumb);
