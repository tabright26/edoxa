import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadUserPhone } from "./actions";

export const withUserPhone = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadUserPhone();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      phone: state.user.phone
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      loadUserPhone: () => dispatch(loadUserPhone())
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
