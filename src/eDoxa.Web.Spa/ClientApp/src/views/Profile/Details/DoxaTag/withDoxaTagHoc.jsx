import React, { Component } from "react";
import { connect } from "react-redux";
import { loadDoxatag } from "../../../../store/actions/identityActions";

const withDoxaTagHoc = WrappedComponent => {
  class DoxaTagContainer extends Component {
    componentDidMount() {
      this.props.actions.loadDoxatag();
    }

    render() {
      return <WrappedComponent doxaTag={this.props.doxaTag} />;
    }
  }

  const mapStateToProps = state => {
    return {
      doxaTag: state.user.doxaTag
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadDoxatag: () => dispatch(loadDoxatag())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(DoxaTagContainer);
};

export default withDoxaTagHoc;
