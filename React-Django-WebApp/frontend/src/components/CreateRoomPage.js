import React, { Component } from "react";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid";
import Typography from "@material-ui/core/Typography";
import TextField from "@material-ui/core/TextField";
import FormHelperText from "@material-ui/core/FormHelperText";
import FormControl from "@material-ui/core/FormControl";
import { Link } from "react-router-dom";
import Radio from "@material-ui/core/Radio";
import RadioGroup from "@material-ui/core/RadioGroup";
import FormControlLabel from "@material-ui/core/FormControlLabel";

export default class CreateRoomPage extends Component {
  defaultHint = 2;
  defaultAnswer = 2;

  constructor(props) {
    super(props);
    this.state = {
      isAlive: true,
      hint: this.defaultHint,
      answer: this.defaultAnswer
    };

    this.handleRoomButtonPressed = this.handleRoomButtonPressed.bind(this);
    this.handleHintChange = this.handleHintChange.bind(this);
    this.handleisAliveChange = this.handleisAliveChange.bind(this);
    this.handleAnswerChange = this.handleAnswerChange.bind(this);
  }

  handleHintChange(e) {
    this.setState({
      hint: e.target.value,
    });
  }

  handleAnswerChange(e) {
    this.setState({
      answer: e.target.value,
    });
  }

  handleisAliveChange(e) {
    this.setState({
      isAlive: e.target.value === "true" ? true : false,
    });
  }

  handleRoomButtonPressed() {
    const requestOptions = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        isAlive: this.state.isAlive,
        answer: this.state.answer,
        hint: this.state.hint
      }),
    };
    fetch("/api/create-room", requestOptions)
      .then((response) => response.json())
      .then((data) => this.props.history.push("/room/" + data.code));
  }

  render() {
    return (
      <Grid container spacing={1}>
        <Grid item xs={8} align="center">
          <br></br><br></br>
          <Typography component="h4" variant="h4">
            Guessing Game
          </Typography>
        </Grid>
        <Grid item xs={8} align="center">
          <FormControl component="fieldset">
            <FormHelperText>
              <div align="center">Whether the answer is alive or not</div>
            </FormHelperText>
            <RadioGroup
              row
              defaultValue="true"
              onChange={this.handleisAliveChange}
            >
              <FormControlLabel
                value="true"
                control={<Radio color="primary" />}
                label="Living"
                labelPlacement="bottom"
              />
              <FormControlLabel
                value="false"
                control={<Radio color="secondary" />}
                label="Nonliving"
                labelPlacement="bottom"
              />
            </RadioGroup>
          </FormControl>
        </Grid>
        <Grid item xs={8} align="center">
          <TextField
            error={this.state.error}
            label="Hint"
            placeholder="Enter Hint"
            value={this.state.roomCode}
            helperText={this.state.error}
            variant="outlined"
            onChange={this.handleHintChange}
          />
        </Grid>
        <Grid item xs={8} align="center">
          <TextField
            error={this.state.error}
            label="Answer"
            placeholder="Enter Answer"
            value={this.state.roomCode}
            helperText={this.state.error}
            variant="outlined"
            onChange={this.handleAnswerChange}
          />
        </Grid>
        <Grid item xs={8} align="center">
          <Button
            color="primary"
            variant="contained"
            onClick={this.handleRoomButtonPressed}
          >
            Start Game
          </Button>
        </Grid>
        <Grid item xs={8} align="center">
          <Button color="secondary" variant="contained" to="/" component={Link}>
            Back
          </Button>
        </Grid>
      </Grid>
    );
  }
}
