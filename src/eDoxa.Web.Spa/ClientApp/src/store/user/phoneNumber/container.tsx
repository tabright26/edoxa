import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { AppState } from "store/types";

const connectUserPhoneNumber = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, phoneNumber, ...attributes }) => <ConnectedComponent actions={actions} phoneNumber={phoneNumber} {...attributes} />;

  const mapStateToProps = (state: AppState) => {
    return {
      phoneNumber: "4301233494"
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

export default connectUserPhoneNumber;
