import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadUserDoxatagHistory, updateUserDoxatag } from "store/root/user/doxatagHistory/actions";
import { RootState } from "store/root/types";

export const withUserDoxatagHistory = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.actions.loadDoxatagHistory();
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
      actions: {
        loadDoxatagHistory: () => dispatch(loadUserDoxatagHistory()),
        changeDoxaTag: (data: any) => dispatch(updateUserDoxatag(data)).then(() => dispatch(loadUserDoxatagHistory()))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
