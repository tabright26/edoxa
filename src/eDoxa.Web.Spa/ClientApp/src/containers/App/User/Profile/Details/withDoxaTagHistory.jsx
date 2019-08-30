import React, { Component } from "react";
import { connect } from "react-redux";
import { loadDoxaTagHistory, changeDoxaTag, CHANGE_DOXATAG_SUCCESS, CHANGE_DOXATAG_FAIL } from "../../../../../store/actions/identityActions";
import { SubmissionError } from "redux-form";

const withDoxaTagHistory = WrappedComponent => {
  class DoxaTagHistoryContainer extends Component {
    async componentDidMount() {
      await this.props.actions.loadDoxaTagHistory();
    }

    render() {
      return <WrappedComponent {...this.props} />;
    }
  }

  const mapStateToProps = state => {
    const doxaTagHistory = state.user.doxaTagHistory.sort((left, right) => (left.timestamp < right.timestamp ? 1 : -1));
    return {
      doxaTag: doxaTagHistory[0] || null
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadDoxaTagHistory: async () => dispatch(loadDoxaTagHistory()),
        changeDoxaTag: async data =>
          await dispatch(changeDoxaTag(data)).then(async action => {
            switch (action.type) {
              case CHANGE_DOXATAG_SUCCESS:
                await dispatch(loadDoxaTagHistory());
                break;
              case CHANGE_DOXATAG_FAIL:
                const { isAxiosError, response } = action.error;
                if (isAxiosError) {
                  throw new SubmissionError(response.data.errors);
                }
                break;
              default:
                console.error(action);
                break;
            }
          })
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(DoxaTagHistoryContainer);
};

export default withDoxaTagHistory;
