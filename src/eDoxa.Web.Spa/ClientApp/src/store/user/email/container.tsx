import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { AppState } from "store/types";

export const connectUserEmail = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, email, ...attributes }) => <ConnectedComponent actions={actions} email={email} {...attributes} />;

  const mapStateToProps = (state: AppState) => {
    return {
      email: "admin@edoxa.gg"
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {}
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
