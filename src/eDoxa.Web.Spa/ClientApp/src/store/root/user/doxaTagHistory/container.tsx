import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadDoxatagHistory, changeDoxaTag } from "store/root/user/doxatagHistory/actions";
import { RootState } from "store/root/types";

export const withUserDoxatagHistory = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, doxaTag, ...attributes }) => {
    useEffect((): void => {
      actions.loadDoxatagHistory();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent actions={actions} doxaTag={doxaTag} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    const doxaTagHistory = state.user.doxatagHistory.data.sort((left, right) => (left.timestamp < right.timestamp ? 1 : -1));
    return {
      doxaTag: doxaTagHistory[0] || null
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadDoxatagHistory: () => dispatch(loadDoxatagHistory()),
        changeDoxaTag: (data: any) => dispatch(changeDoxaTag(data)).then(() => dispatch(loadDoxatagHistory()))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
