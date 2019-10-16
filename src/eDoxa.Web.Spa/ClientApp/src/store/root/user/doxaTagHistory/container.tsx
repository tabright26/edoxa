import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadUserDoxatagHistory } from "store/root/user/doxatagHistory/actions";
import { RootState } from "store/root/types";

export const withUserDoxatag = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadUserDoxatagHistory();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    const doxatagHistory = state.user.doxatagHistory.data.sort((left, right) => (left.timestamp < right.timestamp ? 1 : -1));
    return {
      doxatag: doxatagHistory[0] || null
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      loadUserDoxatagHistory: () => dispatch(loadUserDoxatagHistory())
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
