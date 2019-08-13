import React, { Component } from "react";
import { connect } from "react-redux";
import { loadPersonalInfo } from "../../../../store/actions/identityActions";

const withPersonalInfoHoc = WrappedComponent => {
  class PersonalInfoContainer extends Component {
    componentDidMount() {
      this.props.actions.loadPersonalInfo();
    }

    render() {
      return <WrappedComponent personalInfo={this.props.personalInfo} />;
    }
  }

  const mapStateToProps = state => {
    return {
      personalInfo: state.user.personalInfo
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadPersonalInfo: () => dispatch(loadPersonalInfo())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(PersonalInfoContainer);
};

export default withPersonalInfoHoc;
