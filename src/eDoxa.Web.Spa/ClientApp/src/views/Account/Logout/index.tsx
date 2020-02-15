import React, { FunctionComponent, useEffect } from "react";
import { connect, MapDispatchToProps, MapStateToProps } from "react-redux";
import { logoutUserAccount } from "store/actions/identity";
import { RouteComponentProps, withRouter, Redirect } from "react-router-dom";
import { compose } from "recompose";
import queryString from "query-string";
import { RootState } from "store/types";
import { getDefaultPath } from "utils/coreui/constants";
import { Loading } from "components/Shared/Loading";
import { AccountLogoutToken } from "types/identity";

type StateProps = {
  token: AccountLogoutToken;
};

type DispatchProps = {
  logoutUserAccount: () => void;
};

type OwnProps = RouteComponentProps;

type InnerProps = StateProps & DispatchProps & OwnProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Logout: FunctionComponent<Props> = ({ token, logoutUserAccount }) => {
  useEffect((): void => {
    logoutUserAccount();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  if (token) {
    return (
      <>
        {token.signOutIFrameUrl && (
          <iframe
            title="signout"
            className="signout"
            src={token.signOutIFrameUrl}
            width={0}
            height={0}
          />
        )}
        {token.postLogoutRedirectUri ? (
          <Redirect to={token.postLogoutRedirectUri} />
        ) : (
          <Redirect to={getDefaultPath()} />
        )}
      </>
    );
  }
  return <Loading />;
};

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = state => {
  return {
    token: state.root.user.account.logout.token
  };
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch: any,
  ownProps
) => {
  const { logoutId } = queryString.parse(ownProps.location.search);
  return {
    logoutUserAccount: () => dispatch(logoutUserAccount(logoutId))
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  connect(mapStateToProps, mapDispatchToProps)
);

export default enhance(Logout);
