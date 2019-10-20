import React, { FunctionComponent } from "react";
import MoneyBreadcrumb from "./Money";
import TokenBreadcrumb from "./Token";
import { withUserIsAuthenticated } from "store/root/user/container";
import { compose } from "recompose";

interface InnerProps {
  isAuthenticated: boolean;
}

interface OutterProps {}

type Props = InnerProps & OutterProps;

const AppBreadcrumb: FunctionComponent<Props> = ({ isAuthenticated }) =>
  isAuthenticated && (
    <nav className="breadcrumb d-flex">
      <div className="ml-auto">
        <MoneyBreadcrumb />
      </div>
      <div className="ml-3">
        <TokenBreadcrumb />
      </div>
    </nav>
  );

const enhance = compose<InnerProps, OutterProps>(withUserIsAuthenticated);

export default enhance(AppBreadcrumb);
