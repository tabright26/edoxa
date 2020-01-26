import React, { FunctionComponent, useEffect } from "react";
import { connect, MapDispatchToProps, DispatchProp } from "react-redux";
import { logoutUserAccount } from "store/actions/identity";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { compose } from "recompose";
import queryString from "query-string";
import { LogoutUserAccountAction } from "store/actions/identity/types";
import { push } from "connected-react-router";

type DispatchProps = {
  logoutUserAccount: () => Promise<LogoutUserAccountAction>;
};

type OwnProps = RouteComponentProps;

type InnerProps = DispatchProp & DispatchProps & OwnProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Logout: FunctionComponent<Props> = ({ dispatch, logoutUserAccount }) => {
  useEffect((): void => {
    logoutUserAccount();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return <></>;
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch: any,
  ownProps
) => {
  console.log(ownProps);
  const { logoutId } = queryString.parse(ownProps.location.search);
  return {
    logoutUserAccount: () =>
      dispatch(logoutUserAccount(logoutId)).then(action => {
        var iframe: any = document.createElement("iframe");
        iframe.width = 0;
        iframe.height = 0;
        iframe.class = "signout";
        iframe.src = action.payload.data.signOutIFrameUrl;
        document.getElementById("logout_iframe").appendChild(iframe);
      })
    // .then(() => dispatch(push("/")))
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  connect(null, mapDispatchToProps)
);

export default enhance(Logout);
