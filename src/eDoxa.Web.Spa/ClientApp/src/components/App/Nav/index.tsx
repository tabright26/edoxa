import React, { FunctionComponent } from "react";
import MoneyBreadcrumb from "components/Balance/Money";
import TokenBreadcrumb from "components/Balance/Token";
import { compose } from "recompose";
import {
  withUserIsAuthenticated,
  HocUserIsAuthenticatedStateProps
} from "utils/oidc/containers";

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
