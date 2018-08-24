require 'space_invaders/base'
require 'space_invaders/blocks/u_block'

module SpaceInvaders
  class UBlockContainer < Base

    attr_reader :u_blocks

    def initialize app
      super
      initialize_u_blocks
    end

    def update
      @u_blocks.each { |item| item.update }
    end

    def draw
      @u_blocks.each {|item| item.draw }
    end

    def initialize_u_blocks
      @u_blocks = [
        UBlock.new(app, 150, 400),
        UBlock.new(app, 350, 400),
        UBlock.new(app, 550, 400)
      ]
    end

    def each(&block)
      @u_blocks.each(&block)
    end

    def delete(item)
      @u_blocks.delete(item)
    end

    def <<(item)
      @u_blocks << item
    end

    alias_method :push, :<<
    alias_method :add, :<<

  end
end