import React from "react";
import BreadcrumbMoney from "./Money";
import BreadcrumbToken from "./Token";
import { connectUser } from "store/user/container";

const Breadcrumb: any = ({ isAuthenticated }) => {
  if (isAuthenticated) {
    return (
      <nav className="breadcrumb d-flex">
        <div className="ml-auto">
          <BreadcrumbMoney />
        </div>
        <div className="ml-3">
          <BreadcrumbToken />
        </div>
      </nav>
    );
  }
  return <></>;
};

export default connectUser(Breadcrumb);
