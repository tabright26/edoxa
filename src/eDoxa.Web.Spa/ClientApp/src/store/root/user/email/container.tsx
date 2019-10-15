import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadEmail } from "./actions";

export const withUserEmail = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, email, emailVerified, ...attributes }) => {
    useEffect((): void => {
      actions.loadEmail();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent actions={actions} email={email} emailVerified={emailVerified} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      email: state.user.email.data.email,
      emailVerified: state.user.email.data.emailVerified
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
