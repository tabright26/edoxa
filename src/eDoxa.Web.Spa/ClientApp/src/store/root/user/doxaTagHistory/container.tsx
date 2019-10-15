import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadDoxaTagHistory, changeDoxaTag } from "store/root/user/doxaTagHistory/actions";
import { RootState } from "store/root/types";

export const withUserDoxaTagHistory = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, doxaTag, ...attributes }) => {
    useEffect((): void => {
      actions.loadDoxaTagHistory();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} doxaTag={doxaTag} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    const doxaTagHistory = state.user.doxaTagHistory.data.sort((left, right) => (left.timestamp < right.timestamp ? 1 : -1));
    return {
      doxaTag: doxaTagHistory[0] || null
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadDoxaTagHistory: () => dispatch(loadDoxaTagHistory()),
        changeDoxaTag: (data: any) => dispatch(changeDoxaTag(data)).then(() => dispatch(loadDoxaTagHistory()))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
