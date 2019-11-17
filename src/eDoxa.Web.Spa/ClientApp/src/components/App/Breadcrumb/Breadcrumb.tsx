import React, { FunctionComponent } from "react";
import MoneyBreadcrumb from "./Money";
import TokenBreadcrumb from "./Token";
import { withUserIsAuthenticated } from "store/root/user/container";
import { compose } from "recompose";
import { Breadcrumb } from "reactstrap";

interface InnerProps {
  isAuthenticated: boolean;
}

interface OutterProps {}

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
