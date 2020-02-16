import React, { FunctionComponent } from "react";
import MoneyBreadcrumb from "components/Service/Cashier/Balance/Money";
import TokenBreadcrumb from "components/Service/Cashier/Balance/Token";
import { compose } from "recompose";
import { withUserIsAuthenticated } from "utils/oidc/containers";
import { HocUserIsAuthenticatedStateProps } from "utils/oidc/containers/types";

type InnerProps = HocUserIsAuthenticatedStateProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Nav: FunctionComponent<Props> = ({ isAuthenticated }) =>
  isAuthenticated && (
    <div className="ml-auto mr-3">
      <MoneyBreadcrumb className="d-block" />
      <TokenBreadcrumb className="d-block" />
    </div>
  );

const enhance = compose<InnerProps, OutterProps>(withUserIsAuthenticated);

export default enhance(Nav);
