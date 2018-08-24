require 'space_invaders/base'
require 'space_invaders/behaviors/centerable'

module SpaceInvaders
  class WelcomeScreen < Base
    include Centerable

    attr_reader :welcome_message, :control_index, :press_play

    def initialize app
      super
      @welcome_message = Gosu::Image.from_text app, "SpaceInvaders.rb", App::DEFAULT_FONT, 50
      @control_index = Gosu::Image.from_text app, control_index_string, App::DEFAULT_FONT, 20
      @press_play = Gosu::Image.from_text app, "PRESS SPACE TO PLAY", App::DEFAULT_FONT, 30
      @press_play_counter = 0
    end

    def draw
      horizontal_center_draw welcome_message, 100
      horizontal_center_draw control_index, 200


      horizontal_center_draw press_play, 350 if press_play_counter.between?(30,60)
      update_press_play_counter
    end

    private

      attr_reader :press_play_counter

      def control_index_string
        "      :space => :fire      \n" +
        " :left_arrow => :move_left \n" +
        ":right_arrow => :move_right\n"
      end

      def update_press_play_counter
        if press_play_counter == 60
          @press_play_counter = 0 
        else
          @press_play_counter += 1
        end
      end

  end
end