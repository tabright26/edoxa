import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { RootState } from "store/types";
import { loadEmail } from "./actions";

export const connectUserEmail = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, email, emailVerified, ...attributes }) => {
    useEffect((): void => {
      actions.loadEmail();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} email={email} emailVerified={emailVerified} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      email: state.user.email.email,
      emailVerified: state.user.email.emailVerified
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadEmail: () => dispatch(loadEmail())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
