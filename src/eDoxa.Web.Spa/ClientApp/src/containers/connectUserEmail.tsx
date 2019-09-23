import React, { FunctionComponent } from "react";
import { connect } from "react-redux";

const connectUserEmail = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, email, ...attributes }) => <ConnectedComponent actions={actions} email={email} {...attributes} />;

  const mapStateToProps = () => {
    return {
      email: "admin@edoxa.gg"
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {}
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectUserEmail;
