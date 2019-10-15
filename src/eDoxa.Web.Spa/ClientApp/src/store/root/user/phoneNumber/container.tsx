import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadPhoneNumber, changePhoneNumber } from "./actions";

export const withUserPhone = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, phoneNumber, phoneNumberVerified, ...attributes }) => {
    useEffect((): void => {
      actions.loadPhoneNumber();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} phoneNumber={phoneNumber} phoneNumberVerified={phoneNumberVerified} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      phoneNumber: state.user.phoneNumber.phoneNumber,
      phoneNumberVerified: state.user.phoneNumber.phoneNumberVerified
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadPhoneNumber: () => dispatch(loadPhoneNumber()),
        changePhoneNumber: fields => dispatch(changePhoneNumber(fields.phoneNumber)).then(() => dispatch(loadPhoneNumber()))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
