import React, { Component } from "react";
import { connect } from "react-redux";
import { loadDoxaTagHistory } from "../../../../../store/actions/identityActions";

const withDoxaTagHistory = WrappedComponent => {
  class DoxaTagHistoryContainer extends Component {
    componentDidMount() {
      this.props.actions.loadDoxaTagHistory();
    }

    render() {
      return <WrappedComponent {...this.props} />;
    }
  }

  const mapStateToProps = state => {
    const doxaTagHistory = state.user.doxaTagHistory.sort((left, right) => (left.timestamp < right.timestamp ? 1 : -1));
    return {
      doxaTag: doxaTagHistory[0] || { name: "", code: 0 }
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadDoxaTagHistory: () => dispatch(loadDoxaTagHistory())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(DoxaTagHistoryContainer);
};

export default withDoxaTagHistory;
