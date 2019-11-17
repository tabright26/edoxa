import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadUserDoxatagHistory } from "store/root/user/doxatagHistory/actions";
import { RootState } from "store/types";

export const withUserDoxatagHistory = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadUserDoxatagHistory();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      doxatagHistory: state.root.user.doxatagHistory
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

export const withUserDoxatag = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadUserDoxatagHistory();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    const { data, error, loading } = state.root.user.doxatagHistory;
    const doxatag = data.slice().sort((left: any, right: any) => (left.timestamp < right.timestamp ? 1 : -1))[0] || null;
    return {
      doxatag: {
        data: doxatag,
        error,
        loading
      }
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
