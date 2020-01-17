import React, { FunctionComponent } from "react";
import MoneyBreadcrumb from "./Money";
import TokenBreadcrumb from "./Token";
import { Breadcrumb } from "reactstrap";
import { compose } from "recompose";
import {
  withUserIsAuthenticated,
  HocUserIsAuthenticatedStateProps
} from "utils/oidc/containers";

type InnerProps = HocUserIsAuthenticatedStateProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const AppBreadcrumb: FunctionComponent<Props> = ({ isAuthenticated }) =>
  isAuthenticated && (
    <Breadcrumb listClassName="m-0">
      <li className="breadcrumb-menu">
        <div className="btn-group" role="group">
          <MoneyBreadcrumb />
          <TokenBreadcrumb className="ml-3" />
        </div>
      </li>
    </Breadcrumb>
  );

const enhance = compose<InnerProps, OutterProps>(withUserIsAuthenticated);

export default enhance(AppBreadcrumb);
