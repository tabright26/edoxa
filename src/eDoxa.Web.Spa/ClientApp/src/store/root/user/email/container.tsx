import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadUserEmail } from "./actions";

export const withUserEmail = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadUserEmail();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      email: state.user.email
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      loadUserEmail: () => dispatch(loadUserEmail())
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
