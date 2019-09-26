import React, { FunctionComponent } from "react";
import BreadcrumbMoney from "./Money";
import BreadcrumbToken from "./Token";

const Breadcrumb: FunctionComponent = () => (
  <nav className="breadcrumb d-flex">
    <div className="ml-auto">
      <BreadcrumbMoney />
    </div>
    <div className="ml-3">
      <BreadcrumbToken />
    </div>
  </nav>
);

export default Breadcrumb;
